import { useState, useEffect } from "react";
import { Controller, useForm } from "react-hook-form";
import ModalWrapper from "./ModalWrapper";
import {
  FormControl,
  InputLabel,
  Select,
  CircularProgress,
  FormHelperText,
  MenuItem,
  Autocomplete,
  TextField,
} from "@mui/material";
import { toast } from "react-toastify";
import { IInventoryItem, ILendItemsFormInputs } from "types";
import { usePostLentItemMutation } from "../services/lentItemsService";
import { useGetEmployeesQuery } from "../utils/baseEmployeesApi";

interface LendForHomeFormProps {
  product: IInventoryItem;
  setProducts: React.Dispatch<React.SetStateAction<IInventoryItem[]>>;
  isLendForHomeForm: boolean;
  setIsLendForHomeForm: React.Dispatch<React.SetStateAction<boolean>>;
}

const LendForHomeForm = ({
  product,
  setProducts,
  isLendForHomeForm,
  setIsLendForHomeForm,
}: LendForHomeFormProps) => {
  const [lendItem] = usePostLentItemMutation();
  const [users, setUsers] = useState<{ label: string; value: string }[]>([]);
  const { data: employees } = useGetEmployeesQuery();

  useEffect(() => {
    if (employees) {
      setUsers(
        employees.map((e) => ({
          label: e.name,
          value: e.email,
        }))
      );
    }
  }, [employees]);

  const onSubmit = async (data: ILendItemsFormInputs): Promise<void> => {
    
    const newData = {
      qty: data.qty,
      lentBy: data.lentBy?.value,
      productId: product.id,
    };

    const response = await lendItem(newData);

    if (!("error" in response)) {
      const newProduct = {
        ...product,
        lendQty: product.lendQty - (newData.qty as number),
        combinedQty: product.combinedQty - (newData.qty as number),
      } as IInventoryItem;

      setProducts((oldProducts) =>
        oldProducts.map((p: IInventoryItem) =>
          p.id !== newProduct.id ? p : { ...newProduct }
        )
      );
      toast.success("Successfully lent item!");
      setIsLendForHomeForm(false);
    }
  };

  const {
    handleSubmit,
    formState: { errors, isSubmitting },
    control,
  } = useForm<ILendItemsFormInputs>({
    defaultValues: {
      lentBy: null,
      qty: null,
    },
  });

  return (
    <ModalWrapper open={isLendForHomeForm} setOpen={setIsLendForHomeForm}>
      <div className="lend-item-modal">
        <form
          className=" lend-item-form"
          action="POST"
          onSubmit={handleSubmit(onSubmit)}
        >
          <a
            className="close-modal-button"
            onClick={() => setIsLendForHomeForm(false)}
          >
            <svg
              width={18}
              height={18}
              viewBox="0 0 18 18"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                d="M17.7305 2.02734L10.7578 9L17.7305 15.9727L15.9727 17.7305L9 10.7578L2.02734 17.7305L0.269531 15.9727L7.24219 9L0.269531 2.02734L2.02734 0.269531L9 7.24219L15.9727 0.269531L17.7305 2.02734Z"
                fill="black"
              />
            </svg>
          </a>

          <span className="lendTitle">Lend item</span>
          <div className="lendFormWrapper">
            <div className="lendFormFields">
              <Controller
                control={control}
                name="lentBy"
                rules={{
                  required: {
                    value: true,
                    message: "Lend by field is required",
                  },
                }}
                render={({ field: { onChange, value } }) => (
                  <Autocomplete
                    disablePortal
                    id="combo-box-demo"
                    options={users}
                    onChange={(e, item) => {
                      console.log(e);
                      onChange(item);
                    }}
                    renderInput={(params) => (
                      <TextField
                        error={Boolean(errors.lentBy)}
                        helperText={errors.lentBy?.message}
                        variant="standard"
                        value={value}
                        {...params}
                        label="User"
                      />
                    )}
                  />
                )}
              />
              <Controller
                control={control}
                name="qty"
                rules={{
                  required: {
                    value: true,
                    message: "Available QTY field is required",
                  },
                }}
                render={({ field: { onChange, value } }) => (
                  <FormControl
                    variant="standard"
                    className="lendItemField"
                    error={Boolean(errors.qty)}
                  >
                    <InputLabel focused={false}>Available QTY</InputLabel>
                    <Select value={value || ""} onChange={onChange}>
                      {Array(product.lendQty)
                        .fill(1)
                        .map((n, i) => n + i)
                        .map((o) => (
                          <MenuItem value={o} key={o}>
                            {o}
                          </MenuItem>
                        ))}
                    </Select>
                    <FormHelperText>
                      {errors.qty && errors.qty.message}
                    </FormHelperText>
                  </FormControl>
                )}
              />
            </div>

            <div className="lendFormImage">
              <img
                id="addCurrentImg"
                src={
                  product.image
                    ? product.image
                    : "../../images/no_image-placeholder.png"
                }
                alt="noImgPlaceholder"
              />
            </div>
          </div>

          {isSubmitting ? (
            <CircularProgress className="spinning-loader" />
          ) : (
            <button id="lendItemBtn" type="submit">
              Lend
            </button>
          )}
        </form>
      </div>
    </ModalWrapper>
  );
};

export default LendForHomeForm;
