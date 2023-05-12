import { FormEvent, forwardRef, useEffect, useRef, useState } from "react";
import { loadCategories, loadLocations } from "../services/itemsServices";
import { ICategory, ILocation, IProduct } from "../types";
import { postImageById } from "../services/imageServices";
import { makeRequest } from "../services/makeRequest";
import {
  Dialog,
  FormControl,
  IconButton,
  InputLabel,
  MenuItem,
  Select,
  TextField,
} from "@mui/material";
import { TransitionProps } from "@mui/material/transitions";
import Slide from "@mui/material/Slide";

interface AddNewItemlProps {
  onClose: () => void;
}

const Transition = forwardRef(function Transition(
  props: TransitionProps & {
    children: React.ReactElement<any, any>;
  },
  ref: React.Ref<unknown>
) {
  return <Slide direction="right" ref={ref} {...props} />;
});

const AddNewItemForm = ({ onClose }: AddNewItemlProps): JSX.Element => {
  const [imageValue, setImageValue] = useState(
    "../../images/no_image-placeholder.png"
  );
  const [selectOption, setSelectOption] = useState('')
  const [locationOption, setLocationOption] = useState('')


  const [open, setOpen] = useState(true);

  if (!open) {
    onClose();
  }

  const [categories, setCategories] = useState<ICategory[]>([]);
  useEffect(() => {
    const resultFunc = async () => {
      const result: ICategory[] = await loadCategories();
      setCategories(result);
    };
    resultFunc();
  }, []);

  const [locations, setLocations] = useState<ILocation[]>([]);
  useEffect(() => {
    const resultFunc = async () => {
      const locations: ILocation[] = await loadLocations();
      setLocations(locations);
    };
    resultFunc();
  }, []);

  // const input = document.querySelector("#fileUpload") as HTMLInputElement;

  const inputChange = (e) => {
    const target = e.target as HTMLInputElement;
    const files = target.files as FileList;
    const image = URL.createObjectURL(files[0]);
    setImageValue(image);
  };


  const handleRemoveImage = () =>{
    setImageValue('../../images/no_image-placeholder.png')
  }

  const onFormSubmit = async (e: FormEvent) => {
    e.preventDefault();
    const target = e.target as HTMLFormElement;
    const formData = new FormData(target);
    const image = formData.get("image") as File;

    const data = Object.fromEntries(formData);
    const response: IProduct = await makeRequest({
      path: "/Product",
      method: "POST",
      data,
    });
    const productId = response.id;
    if (image.name) {
      const imageFormData = new FormData();
      imageFormData.append("image", image);
      formData.delete("image");
      console.log(imageFormData);
      
      
      await postImageById(productId, imageFormData);
    }
    setOpen(false);
  };

  const inputStyle = {
    fontSize: '12px',
    '.MuiInputBase-root::after' : {
      borderBottom: ' #000'
    }
  }

  return (
    <Dialog
      open={open}
      TransitionComponent={Transition}
      keepMounted
      onClose={() => setOpen(false)}
      aria-describedby="alert-dialog-slide-description"
      className="add-item-modal"
    >
      <div className="add-item-modal">
        <form
          className="add-item-modal add-item-form"
          action="POST"
          onSubmit={onFormSubmit}
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
                name="code"
                className="inputField"
                id="standard-basic"
                label="Code*"
                variant="standard"
                sx={inputStyle}
                InputLabelProps = {{style:{color: '#9A9A9A' }}}
              ></TextField>
              <TextField
                className="inputField"
                type="text"
                id="item-name"
                name="name"
                variant="standard"
                label="Name"
                sx={inputStyle}
                InputLabelProps = {{style:{color: '#9A9A9A' }}}
              />
              <TextField
                id="standard-multiline-static"
                label="Description"
                name="description"
                multiline
                rows={2}
                className="inputField"
                variant="standard"
                sx={inputStyle}
               InputLabelProps = {{style:{color: '#9A9A9A' }}}

              />
              <FormControl variant="standard" sx={inputStyle}>
                <InputLabel focused={false}>Category</InputLabel>
                <Select
                  value = {selectOption}
                  onChange={(e)=> setSelectOption(e.target.value as string)}
                  label="Category"
                  name="categoryId"
                
                >
                  {categories?.map((c) => (
                    <MenuItem value={c.id} key={c.id}>{c.name}</MenuItem>
                  ))}
                </Select>
              </FormControl>
              <FormControl variant="standard" sx={inputStyle} >
                <InputLabel focused={false}>Location</InputLabel>
                <Select

                 value = {locationOption}
                 onChange={(e)=> setLocationOption(e.target.value as string)}
                  label="Location"
                  name="locationId"
                >
                  {locations?.map((l) => (
                    <MenuItem value={l.id} key={l.id}>{l.name}</MenuItem>
                  ))}
                </Select>
              </FormControl>
             
              <TextField
                className="inputField"
                type="number"
                id="item-name"
                name="saleQty"
                variant="standard"
                label="Qty For Sale"
                sx={inputStyle}
                InputLabelProps = {{style:{color: '#9A9A9A' }}}

              />
               <TextField
                className="inputField"
                type="number"
                id="sale-price"
                name="price"
                variant="standard"
                label="Sale price"
                sx={inputStyle}
                InputLabelProps = {{style:{color: '#9A9A9A' }}}

              />
                <TextField
                className="inputField"
                type="number"
                id="quantity-available"
                name="combinedQty"
                variant="standard"
                label="Qty *"
                sx={inputStyle}
                InputLabelProps = {{style:{color: '#9A9A9A' }}}

              />

            </div>
            <div className="imgSection">
              <img id="addCurrentImg" alt="noImgPlaceholder" src={imageValue} />
              <input
                name="image"
                id="fileUpload"
                type="file"
                style={{ display: "none" }}
                onChange={inputChange}
              />
              <div className="img-buttons">
                <label className="upload-button" htmlFor="fileUpload">
                  Upload
                </label>
                <button onClick={handleRemoveImage} id="remove-button" type="button">
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
    </Dialog>
  );
};
export default AddNewItemForm;
