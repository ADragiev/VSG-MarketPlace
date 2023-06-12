import { render, screen } from "test-utils";
import userEvent from "@testing-library/user-event";
import { IMyOrder } from "types";
import { BrowserRouter } from "react-router-dom";
import mockServer from "../mocks/mock-server";
import MyOrder from "pages/My-Orders/MyOrderRow";

const myOrder: IMyOrder = {
    id: 12,
    productName: 'fasd',
    qty: 3,
    price: 33,
    date: 'fdsfs',
    status: 'fsadf'
};
describe("buyPopup", () => {

  beforeAll(() => mockServer.listen());
  afterEach(() => mockServer.resetHandlers());
  afterAll(() => mockServer.close());
  it("should open Reject Popup", async () => {
    render(<BrowserRouter> <MyOrder myOrder={myOrder} /></BrowserRouter>);
    const user = userEvent.setup();
    const buyPopup = document.querySelector(".deleteIcon");
    await user.click(buyPopup);
    expect(screen.getAllByRole("tooltip")[0]).toBeInTheDocument();
  });
});