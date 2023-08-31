import LockPersonIcon from '@mui/icons-material/LockPerson';

const NotAuthorized = (): JSX.Element => {
    return (
      <div className="main">
      <div className="notAuthorized-container">
        <LockPersonIcon className="notAuthorized-icon" />
        <h1>You are not authorized to view this page</h1>
      </div>
      </div>
    );
  };
  
  export default NotAuthorized;
  