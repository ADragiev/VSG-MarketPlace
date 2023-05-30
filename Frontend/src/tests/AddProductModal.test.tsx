import { render, screen } from "test-utils";
import Inventory from "../pages/Inventory/Inventory";
import userEvent from '@testing-library/user-event';
import mockServer from "../mocks/mock-server";

describe("Inventory", () => {

  beforeAll(() => mockServer.listen());
  afterEach(() => mockServer.resetHandlers());
  afterAll(() => mockServer.close());
  
  it("should open AddProductModal", async () => {
    render(<Inventory />);

    const user = userEvent.setup();
    const addProductBtn = screen.getByText('Add new');
    await user.click(addProductBtn);

    expect(screen.getAllByRole("presentation")[0]).toBeInTheDocument();
  });
});
