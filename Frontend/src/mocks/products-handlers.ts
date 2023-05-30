import { rest } from 'msw';

import products from './products-mock.json';

const productHandlers = [

  rest.get(`url`, (_, res, ctx) => {
    return res(ctx.json(products));
  }),
];

export default productHandlers;