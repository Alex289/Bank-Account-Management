import dynamic from 'next/dynamic';
import { useState } from 'react';

import axios from 'axios';

import useAccount from '@/hooks/useAccount';

import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import Box from '@material-ui/core/Box';
import Button from '@material-ui/core/Button';

import { toast } from 'react-toastify';

import Layout from '@/components/layout';

const TextField = dynamic(() => import('@material-ui/core/TextField'));
const DataTable = dynamic(() => import('@/components/accountDatatable'));

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

export default function Account() {
  const [loading, error, data] = useAccount();

  const [bankId, setbankId] = useState('');
  const [interests, setinterests] = useState<number>(0);
  const [interestlimit, setinterestlimit] = useState<number>(0);
  const [money, setmoney] = useState<number>();
  const [accountIdD, setAccountIdD] = useState('');
  const [amountD, setAmountD] = useState<number>(0);
  const [accountIdW, setAccountIdW] = useState('');
  const [amountW, setAmountW] = useState<number>(0);

  const classes = useStyles();

  function handleSubmit(e: any) {
    e.preventDefault();

    axios
      .post(process.env.NEXT_PUBLIC_API_URL + '/account', {
        money: money,
        interestLimit: interestlimit,
        interests: interests,
        bankID: bankId,
      })
      .then((res) =>
        toast.success(
          `ID: ${res.data} (You may need to reload to see new data)`
        )
      )
      .catch((err) => toast(err));
  }

  function handleDeposit(e: any) {
    e.preventDefault();

    axios
      .post(
        process.env.NEXT_PUBLIC_API_URL + '/account/deposit/' + accountIdD,
        {
          amount: amountD,
        }
      )
      .then((res) => toast.success(`Account status: ${res.data}`))
      .catch((err) => toast(err));
  }

  function handleWithdaw(e: any) {
    e.preventDefault();

    axios
      .post(
        process.env.NEXT_PUBLIC_API_URL + '/account/withdraw/' + accountIdW,
        {
          amount: amountW,
        }
      )
      .then((res) => toast.success(`Account status: ${res.data}`))
      .catch((err) => toast(err));
  }

  return (
    <Layout>
      <h1>Account page</h1>
      <Box component="div" mb={4}>
        <form className={classes.root}>
          <TextField
            label="BankID"
            id="bank-id"
            defaultValue=""
            onChange={(e) => setbankId(e.target.value)}
          />
          <TextField
            label="Interests"
            id="interests"
            defaultValue=""
            onChange={(e) => setinterests(Number(e.target.value))}
          />
          <TextField
            label="Interest limit"
            id="interest-limit"
            defaultValue=""
            onChange={(e) => setinterestlimit(Number(e.target.value))}
          />
          <TextField
            label="Money"
            id="money"
            defaultValue=""
            onChange={(e) => setmoney(Number(e.target.value))}
          />
          <Button variant="contained" color="primary" onClick={handleSubmit}>
            Add new account
          </Button>
        </form>
      </Box>
      <DataTable loading={loading} error={error} data={data} />
      <Box component="div" mt={4}>
        <form className={classes.root}>
          <TextField
            label="AccountID"
            id="account-id-deposit"
            defaultValue=""
            onChange={(e) => setAccountIdD(e.target.value)}
          />
          <TextField
            label="Amount"
            id="amount-deposit"
            defaultValue=""
            onChange={(e) => setAmountD(Number(e.target.value))}
          />
          <Button variant="contained" color="primary" onClick={handleDeposit}>
            Deposit
          </Button>
        </form>
      </Box>
      <Box component="div" mt={4} mb={4}>
        <form className={classes.root}>
          <TextField
            label="AccountID"
            id="account-id-withdraw"
            defaultValue=""
            onChange={(e) => setAccountIdW(e.target.value)}
          />
          <TextField
            label="Amount"
            id="amount-withdraw"
            defaultValue=""
            onChange={(e) => setAmountW(Number(e.target.value))}
          />
          <Button variant="contained" color="primary" onClick={handleWithdaw}>
            Withdraw
          </Button>
        </form>
      </Box>
    </Layout>
  );
}
