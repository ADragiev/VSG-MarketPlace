import { ILocation } from "types";
import { baseApi } from "../utils/baseApi";

const GetLocations = "getLocations";

const locationServices = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    [GetLocations]: builder.query<ILocation[],void>({
      query: () => "/Location",
    }),
  }),
});

export const { useGetLocationsQuery } = locationServices;
