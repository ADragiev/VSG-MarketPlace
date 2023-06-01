import { rest } from 'msw';

import locations from './locations-mock.json';

const locationsHandlers = [

  rest.get(`https://auto.loanvantage360.com/internship/EvaluationSystemAngel/Location`, (_, res, ctx) => {
    return res(ctx.json(locations));
  }),
];

export default locationsHandlers;