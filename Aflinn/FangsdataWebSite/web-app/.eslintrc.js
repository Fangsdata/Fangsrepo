module.exports = {
  env: {
    browser: true,
    es6: true,
  },
  extends: [
    'plugin:react/recommended',
    'airbnb',
  ],
  globals: {
    Atomics: 'readonly',
    SharedArrayBuffer: 'readonly',
  },
  parserOptions: {
    ecmaFeatures: {
      jsx: true,
    },
    ecmaVersion: 2018,
    sourceType: 'module',
  },
  plugins: [
    'react',
  ],
  rules: {
    'no-param-reassign': 'off',
    'no-use-before-define': 'off',
    'no-constant-condition': 'off',
    'no-console': 'off',
    'react/jsx-filename-extension': 'off',
    'react/button-has-type': 'off',
    'no-plusplus': 'off',
    'react/no-array-index-key': 'off',
    'jsx-a11y/no-static-element-interactions': 'off',
    'jsx-a11y/no-noninteractive-element-interactions': 'off',
    'jsx-a11y/click-events-have-key-events': 'off',
    'react/forbid-prop-types': 'off',
    'react/no-did-update-set-state': 'off',
    'react/no-unescaped-entities': 'off'
  },
};
