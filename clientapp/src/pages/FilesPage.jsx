import React, { useEffect, useState, useMemo } from 'react';
import { Table } from '../UI/Table';

export const FilesPage = () => {
    const [data, setData] = useState([]);
    const [error, setError] = useState();
    const [loading, setLoading] = useState(true);
    const [tableData, setTableData] = useState([]);

    useEffect(() => {
        populateData();
    }, [])

    useEffect(() => {
        createTableData();
    }, [loading])

    async function populateData() {
        await fetch('api/SoarFile/GetAll', {
            method: 'GET',
            url: '/',
        })
            .then((response) => response.json())
            .then(setData)
            .then(() => setLoading(false))
            .catch(setError);
    }

    function createTableData() {
        if (data.items === undefined) return;

        const tableData = data.items.map((item, index) => {
            return { ...item, subRows: undefined }
        })
        setTableData(tableData);
    }

    const columns = useMemo(
        () => [
            {
                Header: '#',
                columns: [
                    {
                        Header: 'Id',
                        accessor: 'id',
                    },
                    {
                        Header: 'Code',
                        accessor: 'code',
                    },
                ],
            },
            {
                Header: 'Scan Result',
                columns: [
                    {
                        Header: 'Status',
                        accessor: 'status',
                    },
                    {
                        Header: 'System Scan',
                        accessor: 'isSystemScanClean',
                    },
                ],
            },
            {
                Header: 'Info',
                columns: [
                    {
                        Header: 'Sha256',
                        accessor: 'sha256',
                    },
                    {
                        Header: 'Priority',
                        accessor: 'priority',
                    },
                    {
                        Header: 'Resource',
                        accessor: 'resource',
                    },
                ],
            },
        ],
        []
    )

    let contents = tableData.length < 0
            ? <p><em>Loading...</em></p>
            :  <Table columns={columns} data={tableData} />;

    return (
            <div>
                <h3>File incident Table</h3>
                <div>{contents}</div>
            </div>
        )
}
