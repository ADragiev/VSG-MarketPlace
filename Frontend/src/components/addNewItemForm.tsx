import { useState } from "react";
import { useCreateProductMutation } from "../services/productService";
import { usePostImageMutation } from "../services/imageServices";
import {
  CircularProgress,
  FormControl,
  FormHelperText,
  InputLabel,
  MenuItem,
  Select,
  TextField,
} from "@mui/material";
import { Controller, useForm } from "react-hook-form";
import ModalWrapper from "./ModalWrapper";
import { useGetCategoriesQuery } from "../services/categoryService";
import { useGetLocationsQuery } from "../services/locationService";
import { toast } from "react-toastify";
import {
  ICategory,
  IFormInputs,
  IInventoryItem,
  ILocation,
  IReturnedValue,
} from "../types";

interface AddNewItemProps {
  onClose: () => void;
  setProducts: React.Dispatch<React.SetStateAction<IInventoryItem[]>>;
  isAddNewItemFormOpen: boolean;
  setIsAddNewItemFormOpen: React.Dispatch<React.SetStateAction<boolean>>;
}

const AddNewItemForm = ({
  onClose,
  setProducts,
  isAddNewItemFormOpen,
  setIsAddNewItemFormOpen,
}: AddNewItemProps): JSX.Element => {
  const { data: categories } = useGetCategoriesQuery();
  const { data: locations } = useGetLocationsQuery();
  const [createProduct, { isError }] = useCreateProductMutation();

  const [postImage] = usePostImageMutation();

  const {
    register,
    handleSubmit,
    getValues,
    formState: { errors, isSubmitting },
    control,
  } = useForm<IFormInputs>({
    defaultValues: {
      code: "",
      name: "",
      description: "",
      categoryId: "",
      locationId: "",
      saleQty: null,
      lendQty: null,
      price: null,
      combinedQty: null,
      image: "",
    },
  });

  const [imageValue, setImageValue] = useState(
    "../../images/no_image-placeholder.png"
  );
  const onSubmit = async (data: IFormInputs): Promise<void> => {
    const response = (await createProduct(data)) as { data: IReturnedValue };
    const selectedCategory = categories?.filter(
      (c: ICategory) => c.id == Number(data.categoryId)
    )[0] as ICategory;
    const selectedLocation = locations?.filter(
      (l: ILocation) => l.id == Number(data.locationId)
    )[0] as ILocation;

    const responseData = response.data;
    const id = responseData.returnedValue as number;

    const image = getValues("image")[0] as unknown as File;
    if (imageValue != "../../images/no_image-placeholder.png") {
      const imageFormData = new FormData();
      imageFormData.append("image", image);
      const imageUrl = (await postImage({ id, imageFormData })) as {
        data: IReturnedValue;
      };
      const newProduct = {
        ...data,
        id,
        image: imageUrl.data.returnedValue,
        category: selectedCategory.name,
        location: selectedLocation.name,
      } as IInventoryItem;

      setProducts((oldProducts) => [...oldProducts, newProduct]);
    } else {
      const newProduct = {
        ...data,
        id,
        category: selectedCategory.name,
        location: selectedLocation.name,
      } as IInventoryItem;
      setProducts((oldProducts) => [...oldProducts, newProduct]);
    }
    if (!("error" in response)) {
      toast.success("Successfully added item!");
    }
    setIsAddNewItemFormOpen(false);
  };

  const inputChange = (e: React.MouseEvent<HTMLAnchorElement>) => {
    const target = e.target as HTMLInputElement;
    const files = target.files as FileList;
    const image = URL.createObjectURL(files[0]);
    setImageValue(image);
  };

  const handleRemoveImage = () => {
    setImageValue("../../images/no_image-placeholder.png");
  };

  return (
    <ModalWrapper open={isAddNewItemFormOpen} setOpen={setIsAddNewItemFormOpen}>
      <div className="add-item-modal">
        <form
          className="add-item-modal add-item-form"
          action="POST"
          onSubmit={handleSubmit(onSubmit)}
        >
          <div className="row">
            <a role="button" className="close-modal-button" onClick={onClose}>
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
              <Controller
                control={control}
                name="categoryId"
                rules={{
                  required: {
                    value: true,
                    message: "Category field is required",
                  },
                }}
                render={({ field: { onChange, value } }) => (
                  <FormControl
                    variant="standard"
                    className="inputField"
                    error={Boolean(errors.categoryId)}
                  >
                    <InputLabel focused={false}>Category</InputLabel>
                    <Select value={value} label="Category*" onChange={onChange}>
                      {categories?.map((c: ICategory) => (
                        <MenuItem value={c.id} key={c.id}>
                          {c.name}
                        </MenuItem>
                      ))}
                    </Select>
                    <FormHelperText>
                      {errors.categoryId && errors.categoryId.message}
                    </FormHelperText>
                  </FormControl>
                )}
              />
              <Controller
                control={control}
                name="locationId"
                rules={{
                  required: {
                    value: true,
                    message: "Location field is required",
                  },
                }}
                render={({ field: { onChange, value } }) => (
                  <FormControl
                    className="inputField"
                    variant="standard"
                    error={Boolean(errors.locationId)}
                  >
                    <InputLabel focused={false}>Location</InputLabel>
                    <Select label="Location" value={value} onChange={onChange}>
                      {locations?.map((l: ILocation) => (
                        <MenuItem value={l.id} key={l.id}>
                          {l.name}
                        </MenuItem>
                      ))}
                    </Select>
                    <FormHelperText>
                      {errors.locationId && errors.locationId.message}
                    </FormHelperText>
                  </FormControl>
                )}
              />
              <TextField
                className="inputField"
                type="number"
                variant="standard"
                label="Qty For Sale"
                InputLabelProps={{ style: { color: "#9A9A9A" } }}
                error={Boolean(errors.saleQty)}
                helperText={errors.saleQty?.message}
                {...register("saleQty", {
                  min: {
                    value: 0,
                    message: "Qty for sale must be a positive number",
                  },
                })}
              />
              <TextField
                className="inputField"
                type="number"
                variant="standard"
                label="Qty For Lend"
                InputLabelProps={{ style: { color: "#9A9A9A" } }}
                error={Boolean(errors.lendQty)}
                helperText={errors.lendQty?.message}
                {...register("lendQty", {
                  min: {
                    value: 0,
                    message: "Qty for lend must be a positive number",
                  },
                })}
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
                    value: 0,
                    message: "Price must be a positive number",
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
                  min: { value: 1, message: "Qty must be a positive number" },
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
          {isSubmitting && !isError ? (
            <CircularProgress className="spinning-loader" />
          ) : (
            <button role="button" id="submitFormBtn" type="submit">
              Add
            </button>
          )}
        </form>
      </div>
    </ModalWrapper>
  );
};
export default AddNewItemForm;
