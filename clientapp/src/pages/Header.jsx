import React from 'react';
import { Navbar, Container, Nav, NavDropdown } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';

const Header = () => {
    return (
        <>
            <header>
                <Navbar collapseOnSelect expand="lg" bg="dark" variant="dark" className='justify-content-center'>
                    <Container>
                        <Navbar.Toggle aria-controls="responsive-navbar-nav" />
                        <Navbar.Brand href="/">Home</Navbar.Brand>
                        <Navbar.Collapse id="responsive-navbar-nav">
                            <Nav className="me-auto">
                                <LinkContainer to={"/"}>
                                    <Nav.Link>Home</Nav.Link>
                                </LinkContainer>
                                <NavDropdown title="File" id="collasible-nav-dropdown">
                                    <LinkContainer to={"/files"}>
                                        <NavDropdown.Item>Files</NavDropdown.Item>
                                    </LinkContainer>
                                    <LinkContainer to={"/files/add"}>
                                        <NavDropdown.Item>System Virus Scan</NavDropdown.Item>
                                    </LinkContainer>
                                    <LinkContainer to={"/files/report"}>
                                        <NavDropdown.Item>Report</NavDropdown.Item>
                                    </LinkContainer>
                                    {/* <NavDropdown.Divider /> */}
                                </NavDropdown>
                            </Nav>
                        </Navbar.Collapse>
                    </Container>
                </Navbar>
            </header>
        </>
    )
}

export { Header };
