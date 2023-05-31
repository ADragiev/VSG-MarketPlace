import categoriesHandlers from './categories-handlers';
import productHandlers from './products-handlers';


const handlers = [
  ...productHandlers,
...categoriesHandlers
];

export default handlers;