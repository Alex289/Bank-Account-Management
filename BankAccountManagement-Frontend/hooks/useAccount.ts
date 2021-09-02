import { useEffect, useState } from 'react';

import axios from 'axios';

type Data = {
  id: string;
  bankId: string;
  interests: number;
  interestLimit: number;
  money: number;
};

export default function useAccount() {
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [data, setData] = useState<Data | any>([]);

  useEffect(() => {
    axios
      .get(process.env.NEXT_PUBLIC_API_URL + '/account')
      .then((res) => {
        res.data.forEach((elm: any) => {
          setData((data: any) => [
            ...data,
            {
              id: elm.accountID,
              bankId: elm.bankID,
              interests: elm.interests,
              interestLimit: elm.interestLimit,
              money: elm.money,
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
