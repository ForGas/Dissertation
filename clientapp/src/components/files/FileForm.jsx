import React from 'react';
import { Form, FormGroup, Button, Label, Input, Col } from 'reactstrap'
import { useInput } from '../hooks/useInput'

export const FileForm = () => {
    const [fileProps, resetForm] = useInput([]);

    // useEffect(() => {
    //     fetch('')
    //     .then(response => response.json())
    //     .then(setData)
    //     .cath(console.error)
    // }, [])

    const submit = e => {
        e.preventDefault();
        console.log(fileProps.value);
        console.log(fileProps);
        console.log("gg");
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
