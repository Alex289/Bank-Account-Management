import { DataGrid, GridColDef } from '@material-ui/data-grid';

const columns: GridColDef[] = [
  { field: 'id', headerName: 'ID', width: 400 },
  {
    field: 'bankName',
    headerName: 'Name',
    width: 200,
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
