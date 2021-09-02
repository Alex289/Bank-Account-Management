import { ReactNode } from 'react';

import Meta from '@/components/meta';
import ButtonAppBar from '@/components/appbar';

import Container from '@material-ui/core/Container';

import { ToastContainer } from 'react-toastify';

const Layout = ({
  children,
  title,
}: {
  children: ReactNode;
  title?: string;
}) => (
  <>
    <ToastContainer
      position="top-right"
      autoClose={10000}
      hideProgressBar={false}
      newestOnTop={false}
      closeOnClick={false}
      rtl={false}
      pauseOnFocusLoss
      draggable={false}
      pauseOnHover
    />
    <Meta title={title} />
    <ButtonAppBar />
    <Container maxWidth="lg">
      <div>{children}</div>
    </Container>
  </>
);

export default Layout;
