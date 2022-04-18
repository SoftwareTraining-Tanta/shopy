import React, { useEffect, useRef, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { enterUser, getProductUser } from '../../redux/signinSlice'
import {Container, Form, Input, Box, Text,Button, H1, TitlePage,Title, NavLink, BoxLinks, Error} from './SellerSigninStyle'
function SellerSignin() {
    const dispatch = useDispatch()
    const UsernameInput = useRef()
    const PassInput = useRef()
    const [Username, setUsername] = useState('')
    const [Pass, setPass] = useState('')
    const [E1, setE1] = useState('')
    const [E2, setE2] = useState('')
    const [E3, setE3] = useState('')
    const [style, setStyle] = useState()
    const [accept1, setAccept1] = useState(true)
    const [accept2, setAccept2] = useState(true)
    const [responseRequest, setResponseRequest] = useState('')
    const name = sessionStorage.getItem('signSeller');
    useEffect(()=>{
        if (name == responseRequest) {
            setE1('')
            setE2('')
            setE3('')
            setStyle({
                display: 'block'
            })
            UsernameInput.current.value = ''
            PassInput.current.value = ''
            window.location.href = `/sellerhome`
        }else if( responseRequest == 'Wrong password'){
            setE2(responseRequest)
            setE3('Try Again')
            setAccept2(false)
        }else if( responseRequest == `Vendor with username : ${Username} is not found`){
            setE1(responseRequest)
            setE3('Try Again')
            setAccept1(false)
        }
    },[responseRequest])  
  useEffect(()=>{
      if (/[`!@#$%^&*()+\=\[\]{};':"\\|,.<>\/?~]/.test(Username)) {
          setE1('Please Enter Letters And Number And Underscroe And Dash Line Only')
          setAccept1(false)
      }else if (Username.length > 30) {
          setE1('Please Enter Word Contains 30 Letters Only')
          setAccept1(false)
      }else{
        setAccept1(true)
          setE1('')
          setE3('')
      }
  },[Username])

  useEffect(()=>{
      if (/[`!@#$%^&*()+\=\[\]{};':"\\|,.<>\/?~]/.test(Pass)) {
          setE2('Please Enter Letters And Number And Underscroe And Dash Line Only')
          setAccept2(false)
      }else if (Pass.length > 30) {
          setE2('Please Enter Word Contains 30 Letters Only')
          setAccept2(false)
      }else{
        setAccept2(true)
          setE2('')
          setE3('')
      }
  },[Pass])

  const handleClick = (x) =>{
    x.preventDefault();
    if (accept1 == true && accept2 == true) {
        setE3('')
        sessionStorage.setItem('signSeller', `${Username}`);
        fetch(`https://localhost:5001/api/Vendors/signin/${Username}/${Pass}`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
        }) 
        .then(function(response) {
                return response.text().then(function(text) {
                setResponseRequest(text)
            });
        });
    } else {
        setE3('Try Again')
    }
}
return (
  <Container>
      <TitlePage>Sign in Page</TitlePage>
      <Title>{E3}</Title>
      <H1 style={style}>Welcome {Username}</H1>
      <Form onSubmit={handleClick}>
          <BoxLinks>
              <NavLink to='/sellersignin'>Sign in</NavLink>
              <NavLink to='/sellersignup'>Sign up</NavLink>
          </BoxLinks>
          <Box>
              <Text onClick={() => UsernameInput.current.focus()}>Username</Text>
              <Input placeholder='Write Your Username' required onFocus={()=> setStyle({display: 'none'})} type='text' ref={UsernameInput} onChange={(x)=> setUsername(x.target.value)}/>
              <Error>{E1}</Error>
          </Box>
          <Box>
              <Text onClick={() => PassInput.current.focus()}>Password</Text>
              <Input placeholder='Write Your Password' required onFocus={()=> setStyle({display: 'none'})} type='password' ref={PassInput} onChange={(x)=> setPass(x.target.value)}/>
              <Error>{E2}</Error>
          </Box>
          <Button type='submit' value='Submit'/>          
      </Form>
  </Container>
)
}

export default SellerSignin