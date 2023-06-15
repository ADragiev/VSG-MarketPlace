import styled from "@emotion/styled";
import { Box, ClickAwayListener, Fade, Popper } from "@mui/material";
import { SetStateAction, Dispatch } from "react";

type PopperComponentProps = {
  PopperBody: JSX.Element;
  onYes: () => void;
  anchor: HTMLElement | null;
  setAnchor: Dispatch<SetStateAction<HTMLElement | null>>;
};

const StyledPopper = styled(Popper)(() => ({
  '&[data-popper-placement*="bottom"] .arrow': {
    top: -4,
    left: 0,
  },
  '&[data-popper-placement*="top"] .arrow': {
    bottom: -4,
    left: 0,
  },
}));

const arrow = {
  position: "absolute",
  "&::before": {
    backgroundColor: "white",
    content: '""',
    display: "block",
    width: 14,
    height: 14,
    transform: "rotate(45deg)",
  },
};

const PopperComponent = ({
  PopperBody,
  onYes,
  anchor,
  setAnchor,
}: PopperComponentProps) => {
  return (
    <StyledPopper
      open={Boolean(anchor)}
      anchorEl={anchor}
      placement="bottom"
      disablePortal={false}
      transition
      modifiers={[
        {
          name: "flip",
          enabled: true,
          options: {
            altBoundary: true,
            rootBoundary: "document",
            padding: 8,
          },
        },
        {
          name: "arrow",
          enabled: true,
          options: {
            element: ".arrow",
          },
        },
      ]}
    >
      {({ TransitionProps }) => (
        <ClickAwayListener onClickAway={() => setAnchor(null)}>
          <Fade {...TransitionProps} timeout={500}>
            <div>
              <Box component="span" className="arrow" sx={arrow}></Box>
              <div className="popuptext">
                {PopperBody}
                <div className="buttons-container">
                  <button onClick={onYes} className="btnYesNo">
                    YES
                  </button>
                  <button onClick={() => setAnchor(null)} className="btnYesNo">
                    NO
                  </button>
                </div>
              </div>
            </div>
          </Fade>
        </ClickAwayListener>
      )}
    </StyledPopper>
  );
};
export default PopperComponent;
