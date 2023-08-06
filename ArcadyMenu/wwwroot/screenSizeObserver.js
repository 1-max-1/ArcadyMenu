var dotNetHelper;
var screenIsCurrentlyLarge;

screenIsLarge = () => document.querySelector("html").clientWidth >= 768;

window.initializeScreenSizeObserver = objRef => {
	dotNetHelper = objRef;
	screenIsCurrentlyLarge = screenIsLarge();
	return screenIsLarge();
};

window.addEventListener("resize", () => {
	if (screenIsCurrentlyLarge != screenIsLarge()) {
		screenIsCurrentlyLarge = screenIsLarge();
		dotNetHelper.invokeMethodAsync("OnScreenSizeChanged", screenIsCurrentlyLarge);
	}
});