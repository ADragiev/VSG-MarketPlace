import { useState } from "react";
import { useCreateProductMutation } from "../services/productService";
import { usePostImageMutation } from "../services/imageServices";
import {
  FormControl,
  FormHelperText,
  InputLabel,
  MenuItem,
  Select,
  TextField,
} from "@mui/material";

import { useForm } from "react-hook-form";
import ModalWrapper from "./modalWrapper";
import { useGetCategoriesQuery } from "../services/categoryService";
import { useGetLocationsQuery } from "../services/locationService";
import { toast } from "react-toastify";

interface AddNewItemlProps {
  onClose: () => void;
}

const AddNewItemForm = ({ onClose }: AddNewItemlProps): JSX.Element => {
  const [open, setOpen] = useState(true);

  const { data: categories } = useGetCategoriesQuery("");
  const { data: locations } = useGetLocationsQuery("");
  const [createProduct] = useCreateProductMutation();
  const [postImage] = usePostImageMutation();

  const {
    register,
    handleSubmit,
    getValues,
    formState: { errors },
  } = useForm({
    defaultValues: {
      code: "",
      name: "",
      description: "",
      categoryId: "",
      locationId: "",
      saleQty: null,
      price: null,
      combinedQty: "",
      image: "",
    },
  });

  const [imageValue, setImageValue] = useState(
    "../../images/no_image-placeholder.png"
  );
  const onSubmit = async (data) => {
    const response = await createProduct(data);

    const image = getValues("image")[0] as unknown as File;
    if (imageValue != "../../images/no_image-placeholder.png") {
      const imageFormData = new FormData();
      imageFormData.append("image", image);
      const id = response.data;
      await postImage({ id, imageFormData });
    }
    if (response.error) {
      toast.error("Something went wrong! Please try again later...");
    } else {
      toast.success("Successfully added item!");
    }

    setOpen(false);
  };

  const [selectOption, setSelectOption] = useState("");
  const [locationOption, setLocationOption] = useState("");

  if (!open) {
    onClose();
  }

  const inputChange = (e) => {
    const target = e.target as HTMLInputElement;
    const files = target.files as FileList;
    const image = URL.createObjectURL(files[0]);
    setImageValue(image);
  };

  const handleRemoveImage = () => {
    setImageValue("../../images/no_image-placeholder.png");
  };


  return (
    <ModalWrapper open={open} setOpen={setOpen}>
      <div className="add-item-modal">
        <form
          className="add-item-modal add-item-form"
          action="POST"
          onSubmit={handleSubmit(onSubmit)}
        >
          <div className="row">
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
            <div className="left-side">
              <span>Add new item</span>
              <TextField
                className="inputField"
                id="standard-basic"
                label="Code*"
                variant="standard"
                InputLabelProps={{ style: { color: "#9A9A9A" } }}
                error={Boolean(errors.code)}
                helperText={errors.code?.message}
                {...register("code", {
                  required: "Code field is required",
                  minLength: {
                    value: 3,
                    message: "Code must be at least 3 symbols",
                  },
                  maxLength: {
                    value: 50,
                    message: "Code name cannot be longer than 50 characters",
                  },
                })}
              />
              <TextField
                className="inputField"
                type="text"
                id="item-name"
                variant="standard"
                label="Name*"
                InputLabelProps={{ style: { color: "#9A9A9A" } }}
                error={Boolean(errors.name)}
                helperText={errors.name?.message}
                {...register("name", {
                  required: "Name field is required",
                  minLength: {
                    value: 3,
                    message: "Name must be at least 3 symbols",
                  },
                  maxLength: {
                    value: 100,
                    message: "Name name cannot be longer than 100 characters",
                  },
                })}
              />
              <TextField
                id="standard-multiline-static"
                label="Description"
                multiline
                rows={2}
                className="inputField"
                variant="standard"
                InputLabelProps={{ style: { color: "#9A9A9A" } }}
                {...register("description")}
              />
              <FormControl
                variant="standard"
                className="inputField"
                error={Boolean(errors.categoryId)}
              >
                <InputLabel focused={false}>Category</InputLabel>
                <Select
                  value={selectOption}
                  label="Category*"
                  {...register("categoryId", {
                    required: "Category field is required",
                    onChange: (e) => setSelectOption(e.target.value as string),
                  })}
                >
                  {categories?.map((c) => (
                    <MenuItem value={c.id} key={c.id}>
                      {c.name}
                    </MenuItem>
                  ))}
                </Select>
                <FormHelperText>{errors.categoryId?.message}</FormHelperText>
              </FormControl>
              <FormControl className="inputField"  variant="standard" error={Boolean(errors.locationId)}>
                <InputLabel focused={false}>Location</InputLabel>
                <Select
                  value={locationOption}
                  label="Location"
                  {...register("locationId", {
                    required: "Location field is required",
                    onChange: (e) =>
                      setLocationOption(e.target.value as string),
                  })}
                >
                  {locations?.map((l) => (
                    <MenuItem value={l.id} key={l.id}>
                      {l.name}
                    </MenuItem>
                  ))}
                </Select>
                <FormHelperText>{errors.locationId?.message}</FormHelperText>
              </FormControl>

              <TextField
                className="inputField"
                type="number"
                id="item-name"
                variant="standard"
                label="Qty For Sale"
                InputLabelProps={{ style: { color: "#9A9A9A" } }}
                {...register("saleQty")}
              />
              <TextField
                className="inputField"
                type="number"
                id="sale-price"
                variant="standard"
                label="Sale price"
                InputLabelProps={{ style: { color: "#9A9A9A" } }}
                error={Boolean(errors.price)}
                helperText={errors.price?.message}
                {...register("price", {
                  min: {
                    value: 1,
                    message: "Price must be a possitive number",
                  },
                })}
              />
              <TextField
                className="inputField"
                type="number"
                id="quantity-available"
                variant="standard"
                label="Qty *"
                InputLabelProps={{ style: { color: "#9A9A9A" } }}
                error={Boolean(errors.combinedQty)}
                helperText={errors.combinedQty?.message}
                {...register("combinedQty", {
                  required: "Qty field is required",
                  min: { value: 1, message: "Qty must be a possitive number" },
                  validate: (value) =>
                    (value as unknown as number) >=
                      Number(getValues("saleQty")) ||
                    "Qty cannot be lower than Qty for sale",
                })}
              />
            </div>
            <div className="imgSection">
              <img id="addCurrentImg" alt="noImgPlaceholder" src={imageValue} />
              <input
                id="fileUpload"
                type="file"
                style={{ display: "none" }}
                {...register("image", {
                  onChange: (e) => inputChange(e),
                })}
              />
              <div className="img-buttons">
                <label className="upload-button" htmlFor="fileUpload">
                  Upload
                </label>
                <button
                  onClick={handleRemoveImage}
                  id="remove-button"
                  type="button"
                >
                  Remove
                </button>
              </div>
            </div>
          </div>
          <button id="submitFormBtn" type="submit">
            Add
          </button>
        </form>
      </div>
    </ModalWrapper>
  );
};
export default AddNewItemForm;
