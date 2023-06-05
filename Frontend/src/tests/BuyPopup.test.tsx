import { render, screen } from "test-utils";
import userEvent from "@testing-library/user-event";
import Card from "../components/Product/Product";
import { IProduct } from "types";
import { BrowserRouter } from "react-router-dom";
import mockServer from "mocks/mock-server";

const product: IProduct = {
  id: 45,
  category: "Laptops",
  name: "fsdaf",
  price: 50,
  qty: 5,
  saleQty: 5,
  image: "",
  description: "dfsdadfsa",
  location: "Plovdiv",
};
describe("buyPopup", () => {

  beforeAll(() => mockServer.listen());
  afterEach(() => mockServer.resetHandlers());
  afterAll(() => mockServer.close());
  it("should open buyPopup", async () => {
    render(<BrowserRouter> <Card product={product} /></BrowserRouter>);
    const user = userEvent.setup();
    const buyPopup = screen.getByRole("button");
    await user.click(buyPopup);
    expect(screen.getAllByRole("tooltip")[0]).toBeInTheDocument();
  });
});
