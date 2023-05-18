import { baseApi } from "../utils/baseApi";

const GetLocations = "getLocations";

const locationServices = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    [GetLocations]: builder.query({
      query: () => "/Location",
    }),
  }),
});

export const { useGetLocationsQuery } = locationServices;
