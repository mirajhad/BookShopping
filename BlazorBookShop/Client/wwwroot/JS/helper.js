function WelcomeMsg(message) {
    console.log("Hello" + message);
}
function StaticMethodCall() {
    DotNet.invokeMethodAsync("BlazorBookShop.Client", "CallFromJavaScript")
        .then(result => {
            console.log(result);
        })
}
function NonStaticMethodCall(helperObject) {
    helperObject.invokeMethodAsync("CallFromJavaScriptNonStatic")
        .then(result => {
            console.log("Non Static " + result);
        })
}