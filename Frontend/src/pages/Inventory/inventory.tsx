import { useState } from "react";
import AddNewItemForm from "../../components/AddNewItemForm";
import CustomizedTables from "./Table";
import SearchBar from "./SearchBar";
import { FormControl, InputLabel, MenuItem, Select } from "@mui/material";
import { useGetLocationsQuery } from "../../services/locationService";

function Inventory(): JSX.Element {
  const [isAddNewItemFormOpen, setIsAddNewItemFormOpen] = useState(false);
  const [searchQuery, setSearchQuery] = useState("");
  const { data: locations } = useGetLocationsQuery("");

  const [locationValue, setLocationValue] = useState(0);

  const handleLocationChange = (event) => {
    setLocationValue(event.target.value);
    setSearchQuery("");
  };

  const handleSearchInputChange = (event: React.FormEvent<Element>) => {
    setSearchQuery((event.target as HTMLInputElement).value);
  };

  const handleAddNewItemBtn = () => {
    setIsAddNewItemFormOpen(true);
  };

  return (
    <main className="main">
      {isAddNewItemFormOpen && (
        <AddNewItemForm onClose={() => setIsAddNewItemFormOpen(false)} />
      )}
      <div className="table-wrapper">
        <SearchBar onSearchInputChange={handleSearchInputChange} searchQuery={searchQuery} >
          <FormControl variant="standard" sx={{ ml: 3, mr: 3, minWidth: 160 }}>
            <InputLabel id="demo-simple-select-standard-label" >
              Show items from
            </InputLabel>
            <Select
              labelId="demo-simple-select-standard-label"
              id="demo-simple-select-standard"
              label="location"
              onChange={handleLocationChange}
              
            >
              <MenuItem key={0} value={0}>
                All
              </MenuItem>
              {locations?.map((l) => (
                <MenuItem value={l.id} key={l.id}>
                  {l.name}
                </MenuItem>
              ))}
            </Select>
          </FormControl>

          <button
            id="addNewItemBtn"
            type="button"
            onClick={handleAddNewItemBtn}
          >
            <svg
              width={15}
              height={15}
              viewBox="0 0 15 15"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <line y1="7.5" x2={15} y2="7.5" stroke="white" />
              <line x1="7.5" x2="7.5" y2={15} stroke="white" />
            </svg>
            <span>Add new</span>
          </button>
        </SearchBar>
        <CustomizedTables
          searchQuery={searchQuery}
          locationValue={locationValue}
        />
      </div>
    </main>
  );
}

export default Inventory;
