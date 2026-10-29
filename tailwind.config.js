/** @type {import('tailwindcss').Config} */
module.exports = {
  darkMode: 'class',
  content: [
    './Components/**/*.{razor,html,cshtml}',
    './Pages/**/*.{razor,html,cshtml}',
    './Models/**/*.cs',
    './Services/**/*.cs',
    './wwwroot/**/*.{html,js}'
  ],
  theme: {
    extend: {
      colors: {
        brand: {
          teal: '#00A896',
          darkTeal: '#007D72',
          tealLight: '#E8F6F3',
          navy: '#0A2540',
          deepNavy: '#071F36',
          slate: '#607080',
          border: '#DFE6EA',
          bgLight: '#F8F9FA',
          lightBlue: '#EDF3F7',
          navyPanel: '#153B51',
          navyBorder: '#315468',
          footerDivider: '#2A445B',
          mutedIce: '#BBCBD7',
          mintAccent: '#7FE0D3',
        },
        'brand-teal': '#00A896',
        'brand-teal-dark': '#007F73',
        'brand-navy': '#0A2540',
        'brand-navy-light': '#153B51',
        'brand-slate': '#637487',
        'brand-border': '#DFE6EC',
        'brand-mint': '#E8F7F4',
        'teal-brand': '#00A896',
        'teal-dark': '#007F73',
        'navy-primary': '#0A2540',
        'navy-dark': '#000F22',
        'slate-sub': '#637487',
        'border-light': '#DFE6EC',
        'surface-light': '#F8FAFC',
        'sea-teal': '#00F0D0',
        'sea-mint': '#76F4E0',
        'sea-cyan': '#00A896',
        'teal-glow': '#46E5D1',
        'sea-mist': '#A3C5D9',
        'sea-ice': '#E0F2FE',
        'ocean-top': '#0a3a40',
        'ocean-mid': '#092c3e',
        'ocean-deep': '#041C2C',
        'ocean-dark': '#02101B',
      },
      fontFamily: {
        sans: ['Inter', '-apple-system', 'BlinkMacSystemFont', 'Segoe UI', 'Roboto', 'sans-serif'],
        serif: ['Lora', 'Merriweather', 'serif'],
        mono: ['"JetBrains Mono"', 'Fira Code', 'Courier New', 'monospace'],
      },
      boxShadow: {
        'xs': '0 1px 2px 0 rgba(10, 37, 64, 0.05)',
        'elevated': '0px 12px 32px rgba(5, 40, 58, 0.14)',
        'card': '0px 4px 12px rgba(10, 37, 64, 0.03)',
        'card-hover': '0px 8px 20px rgba(10, 37, 64, 0.08)',
      },
      backdropBlur: {
        'xs': '2px',
      }
    },
  },
  plugins: [
    require('@tailwindcss/forms'),
    require('@tailwindcss/container-queries')
  ],
}
