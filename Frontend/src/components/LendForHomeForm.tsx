import { useState } from "react";
import ModalWrapper from "./ModalWrapper";
import {
  FormControl,
  InputLabel,
  Select,
  CircularProgress,
  FormHelperText,
  MenuItem
} from "@mui/material";
import { useForm } from "react-hook-form";
import { toast } from "react-toastify";
import { IInventoryItem, ILendItemsFormInputs } from "types";
import { usePostLentItemMutation } from "../services/lentItemsService";

interface LendForHomeFormProps {
  onClose: () => void;
  product: IInventoryItem;
}

const LendForHomeForm = ({ onClose, product }: LendForHomeFormProps) => {
  const [open, setOpen] = useState(true);
  const [email, setEmail] = useState("");
  const [lendItem] = usePostLentItemMutation()

  const onSubmit = async (data: ILendItemsFormInputs): Promise<void> => {
    const newData = {...data, productId: product.id}
    console.log(newData);
    
    const response = await lendItem(newData)

    console.log(response);
    
    if (!('error' in response)) {
        toast.success("Successfully..!");
      } 
    setOpen(false);
  };

  const {
    register,
    handleSubmit,
    formState: {errors, isSubmitting },
    watch
  } = useForm<ILendItemsFormInputs>({
    defaultValues: {
       lentBy: "",
       qty: 0,
    },
  });

  if (!open) {
    onClose();
  }

  return (
    <ModalWrapper open={open} setOpen={setOpen}>
      <div className="lend-item-modal">
        <form
          className=" lend-item-form"
          action="POST"
          onSubmit={handleSubmit(onSubmit)}
        >
          <a className="close-modal-button" onClick={onClose}>
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
              <FormControl variant="standard" className="lendItemField">
                <InputLabel focused={false}>Lent By</InputLabel>
                <Select
                  label="Lent By"
                  {...register("lentBy", {
                    required: "Lent By field is required",
                    onChange: (e) => setEmail(e.target.value as string),
                  })}
                >
                    <MenuItem value={'SStoyanov@vsgbg.com'} key={1}>
                         SStoyanov@vsgbg.com
                     </MenuItem>
                     {/* <MenuItem value={'SStoyanov@vsgbg.com'} key={1}>
                         Gosho@vsgbg.com
                     </MenuItem> */}
                  {/* {categories?.map((c: ICategory) => (
                      <MenuItem value={c.id} key={c.id}>
                        {c.name}
                      </MenuItem>
                    ))} */}
                </Select>
                <FormHelperText>{watch('lentBy') === '' && errors.lentBy?.message ? errors.lentBy?.message : '' }</FormHelperText>
              </FormControl>
              <FormControl variant="standard" className="lendItemField">
                <InputLabel focused={false}>Available QTY</InputLabel>
                <Select
                  label="qty"
                  {...register("qty", {
                    required: "Available QTY field is required",
                    onChange: (e) => setAvailableQty(e.target.value as number),
                  })}
                >
                     {Array(product.lendQty).fill(1).map((n,i) => n+i).map((o) =>(
                  <MenuItem value={o} key={o}>
                  {o}
                </MenuItem>
                 
                ))}
                  {/* {categories?.map((c: ICategory) => (
                      <MenuItem value={c.id} key={c.id}>
                        {c.name}
                      </MenuItem>
                    ))} */}
                </Select>
                <FormHelperText>{watch('qty') === 0 && errors.qty?.message ? errors.qty?.message : '' }</FormHelperText>
              </FormControl>
            </div>

            <div className="lendFormImage">
              <img
                id="addCurrentImg"
                src={ product.image ? product.image : "../../images/no_image-placeholder.png" }
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
