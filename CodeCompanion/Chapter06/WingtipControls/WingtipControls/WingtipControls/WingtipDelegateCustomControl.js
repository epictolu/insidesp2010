
ExecuteOrDelayUntilScriptLoaded(SayHello2, "sp.js");

function SayHello2() {
  var message = "Hello from a delegate custom control with a .js file";
  SP.UI.Notify.addNotification(message);
}