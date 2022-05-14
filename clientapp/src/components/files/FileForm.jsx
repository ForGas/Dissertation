import React from 'react';
import { Form, FormGroup, Button, Label, Input, Col } from 'reactstrap'
import { useInput } from '../hooks/useInput'

export const FileForm = () => {
    const [fileProps, resetForm] = useInput([]);

    const submit = e => {
        e.preventDefault();

        fetch('/api/soarFile/VirusScan', {
            referrerPolicy: 'no-referrer',
            method: 'POST',
            url: '/',
            'Content-Type': 'application/x-www-form-urlencoded',
            body: JSON.stringify(fileProps.value),
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
                        <Input {...fileProps} id="formFile" type="file" name="file" placeholder="Enter file" className="mb-3" />
                        <Button type="submit" outline color="secondary" onClick={submit}>Send</Button>

                    </Col>
                </FormGroup>
            </Form>
        </>
    );
}
