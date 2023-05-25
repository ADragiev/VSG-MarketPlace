import {  TextField } from "@mui/material";
import { ReactNode } from "react";
import SearchIcon from '@mui/icons-material/Search';

type Props = {
  children: ReactNode;
  onSearchInputChange: (e: React.FormEvent) => void;
  searchQuery: string
};

const SearchBar = ({ children, onSearchInputChange, searchQuery }: Props) => {

  return (
    <div id="search-container">
      <div className="input-div">
      <SearchIcon id="searchIconAnchor"/>
        <TextField
                id="search"
                label="Search..."
                className="inputField"
                variant="standard"
                value={searchQuery}
                InputLabelProps={{ style: { color: "#9A9A9A" } }}
                onInput={onSearchInputChange}
              />
      </div>
      {children}
    </div>
  );
};
export default SearchBar;
