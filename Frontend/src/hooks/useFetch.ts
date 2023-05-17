import { useState, useEffect, useRef } from 'react';
import { makeRequest } from '../services/makeRequest';
const useFetch = ({ path }) => {
  const [data, setData] = useState(null);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);
  const isComponentMountedRef = useRef(true);
  useEffect(() => {
    const loadData = async () => {
      try {
        setIsLoading(true);
        const data = await makeRequest({ path });
        setData(data);
      } catch (err) {
        setError(err);
      } finally {
        setIsLoading(false);
      }
    }
    if (isComponentMountedRef.current) {
      loadData();
    }
    return () => {
      isComponentMountedRef.current = false;
    }
  }, []);
  return {
    data,
    isLoading,
    error
  }
}
export default useFetch