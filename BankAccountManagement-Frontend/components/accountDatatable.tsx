import { DataGrid, GridColDef } from '@material-ui/data-grid';

const columns: GridColDef[] = [
  { field: 'id', headerName: 'ID', width: 300 },
  {
    field: 'bankId',
    headerName: 'BankID',
    width: 300,
  },
  {
    field: 'interests',
    headerName: 'Interests',
    width: 180,
  },
  {
    field: 'interestLimit',
    headerName: 'Interest limit',
    width: 200,
  },
  {
    field: 'money',
    headerName: 'Money',
    width: 180,
  },
];

export default function DataTable({ loading, error, data }: any) {
  return (
    <div style={{ height: 600, width: '100%' }}>
      <DataGrid
        rows={data}
        loading={loading}
        error={error}
        columns={columns}
        pageSize={10}
        checkboxSelection
        disableSelectionOnClick
        rowsPerPageOptions={[10, 25, 50, 100]}
      />
    </div>
  );
}
