import { useEffect, useState } from 'react';

import axios from 'axios';

type Data = {
  id: string;
  bankname: string;
};

export default function useBank() {
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [data, setData] = useState<Data | any>([]);

  useEffect(() => {
    axios
      .get(process.env.NEXT_PUBLIC_API_URL + '/bank')
      .then((res) => {
        res.data.forEach((elm: any) => {
          setData((data: any) => [
            ...data,
            {
              id: elm.bankID,
              bankName: elm.bankName,
            },
          ]);
        });
        setLoading(false);
      })
      .catch((err) => {
        setError(err);
        setLoading(false);
      });
  }, []);

  return [loading, error, data];
}
