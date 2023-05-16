import { Dialog, Slide } from "@mui/material";
import { TransitionProps } from "@mui/material/transitions";
import { ReactNode, forwardRef } from "react";

const Transition = forwardRef(function Transition(
  props: TransitionProps & {
    children: React.ReactElement<any, any>;
  },
  ref: React.Ref<unknown>
) {
  return <Slide direction="right" ref={ref} {...props} />;
});

type ModalWrapperProps = {
    children: ReactNode,
    open: boolean, 
    setOpen: (arg: boolean)=> void
}

const ModalWrapper = ({ children, open, setOpen, }: ModalWrapperProps) => {

    const handleClose = ()=>{
        setOpen(false)
    }
    
  return (
    <Dialog
      open={open}
      TransitionComponent={Transition}
      keepMounted
      onClose={handleClose}
      aria-describedby="alert-dialog-slide-description"
    >
      {children}
    </Dialog>
  );
};

export default ModalWrapper
