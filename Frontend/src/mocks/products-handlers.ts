import { rest } from 'msw';

import products from './products-mock.json';

const productHandlers = [

  rest.get(`https://auto.loanvantage360.com/internship/EvaluationSystemAngel/Product/Inventory`, (_, res, ctx) => {
    return res(ctx.json(products));
  }),
];

export default productHandlers;