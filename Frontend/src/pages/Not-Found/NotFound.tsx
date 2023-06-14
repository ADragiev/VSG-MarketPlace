import SearchOffIcon from "@mui/icons-material/SearchOff";
const NotFound = (): JSX.Element => {
  return (
    <div className="main">

    <div className="notFound-container">
      <SearchOffIcon className="notFound-icon" />
      <h1>404</h1>
      <h2>Page Not Found</h2>
    </div>
    </div>
  );
};

export default NotFound;
