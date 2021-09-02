import Link from 'next/link';

import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import Breadcrumbs from '@material-ui/core/Breadcrumbs';
import MaterialLink from '@material-ui/core/Link';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      flexGrow: 1,
    },
    menuButton: {
      marginRight: theme.spacing(2),
    },
    title: {
      flexGrow: 1,
    },
  })
);

export default function ButtonAppBar() {
  const classes = useStyles();

  return (
    <div className={classes.root}>
      <AppBar position="static">
        <Toolbar>
          <Typography variant="h6" className={classes.title}>
            BankAccountManagement
          </Typography>
          <Breadcrumbs aria-label="breadcrumb">
            <Link passHref href="/">
              <MaterialLink color="inherit">Bank</MaterialLink>
            </Link>
            <Link passHref href="/account">
              <MaterialLink color="inherit">Account</MaterialLink>
            </Link>
          </Breadcrumbs>
        </Toolbar>
      </AppBar>
    </div>
  );
}
