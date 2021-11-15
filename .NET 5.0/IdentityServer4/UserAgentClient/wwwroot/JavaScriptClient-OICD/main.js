
// ===> oidc - clien.js https://github.com/IdentityModel/oidc... (WIKI)
//oidc - clien.js CDN https://cdnjs.com/libraries/oidc-client
//angular - oauth2 - oidc https://github.com/manfredsteyer/angu...
//react - oidc https://github.com/thchia/react-oidc
// ===> axios https://github.com/axios/axios


var config = {
    userStore: new Oidc.WebStorageStateStore({store: window.localStorage}), //Persistence
    authority: "https://localhost:44380/",
    client_id: "client_id_jsOICD",
    redirect_uri: "https://localhost:44379/Home/PrivacyOicdFlow",
    post_logout_redirect_uri: "https://localhost:44379/Home/Index",
    response_type: "code",
    scope: "openid rc.scope ApiOne ApiTwoClient",

};
 
// SIGN IN 
var userManager = new Oidc.UserManager(config);

var signIn = function () {
    userManager.signinRedirect();
};

// SIGN OUT
var signOut = function () {
    userManager.signoutRedirect();
};


// CONFIRM ACCESS TOKEN
userManager.getUser().then(user => {
    console.log("user: ", user);

    if (user) {
        axios.defaults.headers.common['Authorization'] = "Bearer " + user.access_token;
    }
});

var callApi = function () {
    axios.get("https://localhost:44359/api/secret").then(res => {
        console.log(res)
    });
};


// REFRESH TOKEN
var refreshing = false;

axios.interceptors.response.use(
function (response) { return response },
function (error)
{
    console.log("axios error: ", error.response);

    var axiosConfig = error.response.config;

    // if error response 401 try refresh token
    if (error.response.status === 401) {
        console.log("axios error 401");

        // if already refreshing don't make another request
        if (!refreshing) {
            console.log("starting refresh token");
            refreshing = true;

            // do the refresh
            return userManager.signinSilent().then(user => {
                console.log("refresh user: ", user);

                // update the http client
                axios.defaults.headers.common['Authorization'] = "Bearer " + user.access_token;

                // update the http request
                axiosConfig.headers['Authorization'] = "Bearer " + user.access_token;
                
                // retry the http request
                return axios(axiosConfig);
            })
        }
    }
    return Promise.reject(error);
});

