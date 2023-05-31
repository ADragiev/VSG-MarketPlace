import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.tsx'
import { Provider } from 'react-redux'
import  store  from './redux/store.ts'
import { ToastContainer } from 'react-toastify'
import 'react-toastify/dist/ReactToastify.css';
import '../public/styles/styles.scss';

ReactDOM.createRoot(document.getElementById('root') as HTMLElement).render(
  <>
  <React.StrictMode>
    <Provider store={store}>
    <App />
    <ToastContainer position='bottom-right'/>
    </Provider>
  </React.StrictMode>
</>
)
  