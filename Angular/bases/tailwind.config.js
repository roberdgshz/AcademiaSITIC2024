const plugin = require('tailwindcss/plugin');

module.exports = {
  content: [
    "./src/**/*.{html,ts}",
  ],
  important: ':root',
  theme: {
    extend: {},
  },
  corePlugins: {
    container: false,
    preflight: false
  },
  plugins: [
    plugin(function({ addUtilities, addComponents }) {
        addUtilities({
                '.icon-base': {
                    'font-size': '1.5rem !important',
                    'height': '1.5rem !important',
                    'width': '1.5rem !important',
                    'min-width': '1.5rem !important',
                    'min-height': '1.5rem !important',
                    'line-height': '1.5rem !important'
                },
            }),
            addComponents({})
    })
],
}

