import type { AppProps } from 'next/app';
import Router from 'next/router';

import { ThemeProvider } from '@material-ui/core/styles';
import CssBaseline from '@material-ui/core/CssBaseline';
import theme from '@theme';

import 'react-toastify/dist/ReactToastify.css';

import NProgress from 'nprogress';

NProgress.configure({ showSpinner: false });

Router.events.on('routeChangeStart', () => {
  NProgress.start();
});
Router.events.on('routeChangeComplete', () => NProgress.done());
Router.events.on('routeChangeError', () => NProgress.done());

export default function App({ Component, pageProps }: AppProps) {
  return (
    <div>
      <ThemeProvider theme={theme}>
        <CssBaseline></CssBaseline>
        <Component {...pageProps} />
      </ThemeProvider>
      <style jsx global>{`
        #nprogress {
          pointer-events: none;
        }
        #nprogress .bar {
          background: #f50057;
          position: fixed;
          z-index: 1031;
          top: 0;
          left: 0;
          width: 100%;
          height: 2px;
        }
        #nprogress .peg {
          display: block;
          position: absolute;
          right: 0;
          width: 100px;
          height: 100%;
          box-shadow: 0 0 10px #f50057, 0 0 5px #f50057;
          opacity: 1;
          -webkit-transform: rotate(3deg) translate(0, -4px);
          -ms-transform: rotate(3deg) translate(0, -4px);
          transform: rotate(3deg) translate(0, -4px);
        }
        #nprogress .spinner {
          display: block;
          position: fixed;
          z-index: 1031;
          top: 15px;
          right: 15px;
        }
        #nprogress .spinner-icon {
          width: 18px;
          height: 18px;
          box-sizing: border-box;
          border: solid 2px transparent;
          border-top-color: #f50057;
          border-left-color: #f50057;
          border-radius: 50%;
          -webkit-animation: nprogress-spinner 0.4s linear infinite;
          animation: nprogress-spinner 0.4s linear infinite;
        }
        .nprogress-custom-parent {
          overflow: hidden;
          position: relative;
        }
        .nprogress-custom-parent #nprogress .bar,
        .nprogress-custom-parent #nprogress .spinner {
          position: absolute;
        }
        @-webkit-keyframes nprogress-spinner {
          0% {
            -webkit-transform: rotate(0);
          }
          100% {
            -webkit-transform: rotate(360deg);
          }
        }
        @keyframes nprogress-spinner {
          0% {
            transform: rotate(0);
          }
          100% {
            transform: rotate(360deg);
          }
        }
      `}</style>
    </div>
  );
}
