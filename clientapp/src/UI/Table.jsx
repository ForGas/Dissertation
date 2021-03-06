import React from 'react';
import { useTable } from 'react-table'
import BTable from 'react-bootstrap/Table';
import { Container } from 'react-bootstrap';

export const Table = ({ columns, data }) => {
    const { getTableProps, headerGroups, rows, prepareRow } = useTable({
        columns,
        data,
    })

    return (
        <Container>
            <BTable striped bordered hover size="sm" {...getTableProps()}>
                <thead>
                    {headerGroups.map(headerGroup => (
                        <tr {...headerGroup.getHeaderGroupProps()}>
                            {headerGroup.headers.map(column => (
                                <th {...column.getHeaderProps()}>
                                    {column.render('Header')}
                                </th>
                            ))}
                        </tr>
                    ))}
                </thead>
                <tbody>
                    {rows.map((row, i) => {
                        prepareRow(row)
                        return (
                            <tr {...row.getRowProps()}>
                                {row.cells.map(cell => {
                                    return (
                                        <td {...cell.getCellProps()}>
                                            {cell.render('Cell')}
                                        </td>
                                    )
                                })}
                            </tr>
                        )
                    })}
                </tbody>
            </BTable>
        </Container>
    )
}