import { ReactElement } from 'react';
import { render } from '@testing-library/react';
import { Provider } from 'react-redux';

import store from '../redux/store';

type ProvidersProps = {
  children: ReactElement;
};

const AllTheProviders = ({ children } : ProvidersProps) => {
  return (
    <Provider store={store}>
      {children}
    </Provider>
  )
}

const customRender = (ui: ReactElement, options?: object) => (
  render(ui, { wrapper: AllTheProviders, ...options })
);

export * from '@testing-library/react'

// override render method
export { customRender as render }