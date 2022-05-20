import React, { useRef } from 'react';
import { Form, FormGroup, Button, Label, Input, Col } from 'reactstrap'
import { useInput } from '../../hooks/useInput'
import { SYSTEM_VIRUS_SCAN } from '../../../constants/apiUrl'

export const SystemVirusScanForm = () => {
    const [fileProps, resetForm] = useInput([]);
    const fileInputRef = useRef();

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
            .then((data) => {
                console.log(data);
            });

        resetForm();
    }

    return (
        <>
            <Form>
                <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                    <Label>Sending a file for virus scan</Label >
                    <Col sm={5}>
                        <Input innerRef = {fileInputRef}
                            {...fileProps}
                            id="formFile"
                            type="file"
                            name="file"
                            placeholder="Enter file"
                            className="mb-3"
                        />
                        <Button type="submit" outline color="secondary" onClick={onSubmit}>Send</Button>
                    </Col>
                </FormGroup>
            </Form>
        </>
    );
}
