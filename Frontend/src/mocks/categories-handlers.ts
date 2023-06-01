import { rest } from 'msw';

import categiries from './categories-mock.json';

const categoriesHandlers = [

  rest.get(`https://auto.loanvantage360.com/internship/EvaluationSystemAngel/Category`, (_, res, ctx) => {
    return res(ctx.json(categiries));
  }),
];

export default categoriesHandlers;