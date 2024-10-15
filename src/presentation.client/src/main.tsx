import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './app/App.tsx';
import 'xplorer-ui/dist/style.css';
import './lib/types/string-extensions.ts';

import { SimpleModalContextProvider, SimpleConfirmProvider, ThemeProvider, Toaster, TooltipProvider } from 'xplorer-ui';
import './index.css';
import { Provider } from 'react-redux';
import { store } from './lib/store/index.tsx';
import { BrowserRouter as Router } from 'react-router-dom';

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <ThemeProvider defaultTheme="dark" storageKey="bit-hrms-theme">
      <Provider store={store}>
        <Router>
          <TooltipProvider>
            <SimpleModalContextProvider>
              <SimpleConfirmProvider>
                <App />
              </SimpleConfirmProvider>
            </SimpleModalContextProvider>
          </TooltipProvider>
          <Toaster />
        </Router>
      </Provider>
    </ThemeProvider>
  </React.StrictMode>,
);
