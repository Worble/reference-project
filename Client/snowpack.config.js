module.exports = {
	mount: {
		public: "/",
		src: "/_dist_",
	},
	plugins: [
		"@snowpack/plugin-dotenv",
		"@snowpack/plugin-babel",
		"@snowpack/plugin-typescript",
		"@prefresh/snowpack",
		"@snowpack/plugin-webpack",
	],
	install: [
		/* ... */
	],
	installOptions: {
		installTypes: true,
	},
	devOptions: {
		/* ... */
	},
	buildOptions: {
		/* ... */
	},
	proxy: {
		/* ... */
	},
	alias: {
		/* ... */
	},
};
