import categoriesHandlers from './categories-handlers';
import locationsHandlers from './locations-handlers';
import productHandlers from './products-handlers';


const handlers = [
  ...productHandlers,
...categoriesHandlers,
...locationsHandlers
];

export default handlers;