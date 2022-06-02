import React, { useRef, useState } from 'react';
import { Form, FormGroup, Button, Label, Input  } from 'reactstrap'
import { useInput } from '../../hooks/useInput'
import { Container } from 'react-bootstrap';
import { SYSTEM_VIRUS_SCAN } from '../../../constants/apiUrl'
import { useNavigate } from "react-router-dom";
import './systemVirusScan.css'

export const SystemVirusScanForm = () => {
    const [fileProps, resetForm] = useInput([]);
    const fileInputRef = useRef();
    const [data, setData] = useState([]);
    const [error, setError] = useState();
    const [loading, setLoading] = useState(true);
    let navigate = useNavigate();

    const onSubmit = e => {
        e.preventDefault();
        const formData = new FormData();
        formData.append('file', fileInputRef.current.files[0]);

        fetch(SYSTEM_VIRUS_SCAN, {
            method: 'POST',
            url: '/',
            body: formData,
        })
            .then((response) => response.json())
            .then(setData)
            .then(() => setLoading(false))
            .catch(setError);

        resetForm();
    }

    const navigateReport = e => {
        navigate(`/files/report/${data.id}`);
    }

    let contents = loading
        ? <></>
        : <>
            <p className='text'>{data.status}</p>
            <div className='d-flex justify-content-center align-items-center'>
                <Button 
                    onClick={navigateReport} 
                    className="btn btn-primary btn-lg btn-block"
                >
                    Перейти к отчету
                </Button>
            </div>
        </>

    return (
        <>
            <section className="system-scan-virus_wrapper">
                <Container>
                    <Form className="form_style">
                        <FormGroup>
                            <Label className="system-scan-virus_label">Sending a file for virus scan</Label >
                            <Input innerRef={fileInputRef}
                                {...fileProps}
                                id="formFile"
                                type="file"
                                name="file"
                                placeholder="Enter file"
                                className="mb-3"
                            />
                            <Button type="submit" outline color="secondary" onClick={onSubmit}>Send</Button>
                        </FormGroup>
                    </Form>
                </Container>
            </section>

            <section className="system-scan-virus_wrapper">
                <Container>
                    {contents}
                </Container>
            </section>
        </>
    );
}
