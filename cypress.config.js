const { defineConfig } = require('cypress')

module.exports = defineConfig({
  env: {
    apiUrl: 'http://localhost:5000',
  },
  e2e: {
    baseUrl: 'http://localhost:4200',
    setupNodeEvents(on, config) {},
    supportFile: false,
  },
})
