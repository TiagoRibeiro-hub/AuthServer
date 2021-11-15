
var createSession = function () {
    return "KeySessionValueMakeItABitLongerrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrr"
}

var createNonce = function () {
    return "KeyNonceValueeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee"
}

var signInIF = function () {

    var authUrl_Controller = `/connect/authorize/callback`;
    var authUrl_Client_id = `?client_id=client_id_jsIF`;
    var authUrl_Redirect_uri = `&redirect_uri=${encodeURIComponent("https://localhost:44379/Home/PrivacyImplicitFlow")}`;
    var authUrl_Response_type = `&response_type=${encodeURIComponent("id_token token")}`;
    var authUrl_Scope = `&scope=${encodeURIComponent("openid ApiOne")}`;
    var authUrl_Nonce = `&nonce=${createNonce()}`;
    var authUrl_State = `&state=${createSession()}`;

    var authUrl = authUrl_Controller + authUrl_Client_id + authUrl_Redirect_uri + authUrl_Response_type + authUrl_Scope + authUrl_Nonce + authUrl_State

    var returnUrl = encodeURIComponent(authUrl);

    window.location.href = "https://localhost:44380/Auth/LogIn?ReturnUrl=" + returnUrl

}