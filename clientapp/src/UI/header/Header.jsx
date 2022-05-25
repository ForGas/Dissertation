import React from 'react';
import { Navbar, Container, Nav, NavDropdown } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import './header.css'

const Header = () => {
    return (
        <>
            <header>
                <section className='header_wrapper'>
                    <Navbar collapseOnSelect expand="lg" className='justify-content-center'>
                        <Container>
                            <Navbar.Toggle aria-controls="responsive-navbar-nav" />
                            <Navbar.Brand href="/">HOME</Navbar.Brand>
                            <Navbar.Collapse id="responsive-navbar-nav">
                                <Nav className="me-auto">
                                    <LinkContainer to={"/"}>
                                        <Nav.Link>Главная</Nav.Link>
                                    </LinkContainer>
                                    <NavDropdown title="Файлы" id="collasible-nav-dropdown">
                                        <LinkContainer to={"/files"}>
                                            <NavDropdown.Item>Таблица</NavDropdown.Item>
                                        </LinkContainer>
                                        <LinkContainer to={"/files/add"}>
                                            <NavDropdown.Item>System Virus Scan</NavDropdown.Item>
                                        </LinkContainer>
                                        <LinkContainer to={"/files/report"}>
                                            <NavDropdown.Item>Отчет</NavDropdown.Item>
                                        </LinkContainer>
                                        {/* <NavDropdown.Divider /> */}
                                    </NavDropdown>
                                </Nav>
                            </Navbar.Collapse>
                        </Container>
                    </Navbar>
                </section>
            </header>
        </>
    )
}

export { Header };
