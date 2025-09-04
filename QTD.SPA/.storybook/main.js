module.exports = {
  "stories": [
    "../src/**/*.stories.mdx",
    "../src/**/*.stories.@(js|jsx|ts|tsx)"
  ],
  "addons": [
    "@storybook/addon-links",
    "@storybook/addon-essentials"
  ],
  "framework": "@storybook/angular",
  "core": {
    "builder": "webpack5",
  },
  webpackFinal: async (config) => {
    // maybe other configs...

    config.module.rules.push({
      test: /\.(m?js)$/,
      type: "javascript/auto",
      resolve: {
        fullySpecified: false,
      },
    });

    return config;
  },
}
