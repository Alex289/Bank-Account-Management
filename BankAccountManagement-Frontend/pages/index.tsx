import dynamic from 'next/dynamic';
import { useState } from 'react';

import axios from 'axios';

import useBank from '@/hooks/useBank';

import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import Box from '@material-ui/core/Box';
import Button from '@material-ui/core/Button';

import { toast } from 'react-toastify';

import Layout from '@/components/layout';

const TextField = dynamic(() => import('@material-ui/core/TextField'));
const DataTable = dynamic(() => import('@/components/bankDatatable'));

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      '& > *': {
        margin: theme.spacing(1),
        width: '25ch',
      },
    },
  })
);

export default function Index() {
  const [bankName, setbankName] = useState('');
  const [bankId, setbankId] = useState('');

  const [loading, error, data] = useBank();

  const classes = useStyles();

  function handleSubmit(e: any) {
    e.preventDefault();

    axios
      .post(process.env.NEXT_PUBLIC_API_URL + '/bank', {
        bankName: bankName,
      })
      .then((res) =>
        toast.success(
          `ID: ${res.data} (You may need to reload to see new data)`
        )
      )
      .catch((err) => toast(err));
  }

  function handleChangeInterest(e: any) {
    e.preventDefault();

    axios
      .get(process.env.NEXT_PUBLIC_API_URL + '/bank/charge-interests/' + bankId)
      .then((res) => toast.success('Affected accounts: ' + res.data))
      .catch((err) => toast.error(err));
  }

  return (
    <Layout>
      <h1>Bank page</h1>
      <Box component="div" mb={4}>
        <form className={classes.root}>
          <TextField
            label="Bank name"
            id="bank-name"
            defaultValue=""
            onChange={(e) => setbankName(e.target.value)}
          />
          <Button variant="contained" color="primary" onClick={handleSubmit}>
            Add new bank
          </Button>
        </form>
      </Box>
      <Box component="div" mt={4}>
        <DataTable loading={loading} error={error} data={data} />
      </Box>
      <Box component="div" mt={4} mb={4}>
        <form className={classes.root}>
          <TextField
            label="BankID"
            id="bank-id"
            defaultValue=""
            onChange={(e) => setbankId(e.target.value)}
          />
          <Button
            variant="contained"
            color="primary"
            onClick={handleChangeInterest}>
            Charge interests
          </Button>
        </form>
      </Box>
    </Layout>
  );
}
