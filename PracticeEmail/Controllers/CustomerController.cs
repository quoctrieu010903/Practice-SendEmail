﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeEmail.Helpper;
using PracticeEmail.Service;

namespace PracticeEmail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IEmailServices _emailServices;

        public CustomerController(IEmailServices emailServices)
        {
            _emailServices = emailServices;
        }

        [HttpPost("SendMail")]
        public async Task<IActionResult> SendMail([FromBody] MailRequest request) 
        {
            try
            {

                MailRequest mailRequest = new MailRequest();
                mailRequest.ToEmail = request.ToEmail;
                mailRequest.Subject = "FUGoodsExchangesService confirmed Registration";
                mailRequest.Body = EnailContent(mailRequest.ToEmail);
                await _emailServices.SendEmailAsync(mailRequest);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
            return Ok();
        }

        public static string EnailContent(string email)
        {
            {
                string style =
                #region
                    @"<style>
                body {
                    font-family: Arial, sans-serif;
                    margin: 0;
                    padding: 0;
                }
                .container {
                    width: 600px;
                    margin: 0 auto;
                    padding: 20px;
            
                }
                .header {
                    display: flex;
                    background-color: #f7f7f7;
                }
                button {
                display: inline-block;
                border-radius: 4px;
                background-color: #ff7800;
                border: none;
                color: #ffffff;
                text-align: center;
                font-size: 17px;
                padding: 10px;
                width: 150px;
                transition: all 0.5s;
                cursor: pointer;
                margin: 10px;
                }
                .push-button {
                    display: flex;
                    justify-content: center;
                }
                button strong {
                    cursor: pointer;
                    display: inline-block;
                    position: relative;
                    transition: 0.5s;
                }
                button:hover strong {
                    padding-right: 15px;
                }
                button:hover strong:after {
                    opacity: 1;
                    right: 0;
                }
                .footer {
                    background-color: #f7f7f7;
                }
                .info{
                    padding: 5px;
                }
                .info p{
                    font-size: 13px;
                }
            </style>";
                #endregion


                string body =
                    $@"
                <!DOCTYPE html>
                <html lang=""en"">
                <head>
                    <meta charset=""UTF-8"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                    <title>Confirm Email</title>
                    {style}
                </head>
                <body>
                    <div class=""container"">
                        <div class=""header"">
                            <div>
                                <img src=""https://cdn.discordapp.com/attachments/1036663699458490408/1166764222080888893/logo_web_2.png""
                                  alt width=""100px"" />
                            </div>
                            <div style=""border-left: 2px solid #ff7800; margin-left: 20px;"">
                                <h4 style=""margin-left: 20px;"">Xác nhận Email</h4>
                            </div>
                        </div>
                        <div class=""content"">
                            <div>
                                <h3>Xác thực FService Account của bạn</h3>
                                <p style=""font-size: 15px;"">Bạn đã đăng ký {email} tại FService.</p>
                                <p style=""font-size: 15px;"">Để xác thực địa chỉ email của bạn hãy nhấn vào nút bên dưới.</p>
                            </div>
                            <div class=""push-button"">
                                <button>
                                    <a style=""color: white;"" href=""""><strong>XÁC NHẬN</strong></a>
                                </button>
                            </div>
                            <div class=""note"">
                                <p style=""font-size: 15px; font-style: italic;"">* Lưu ý: Tài khoản chỉ có thể đăng nhập được khi đã xác thực.</p>
                            </div>
                        </div>
                        <div class=""footer"">
                            <img
                              src=""https://cdn.discordapp.com/attachments/1036663699458490408/1166764222080888893/logo_web_2.png""
                              alt=""logo"" width=""80px"" />
                            <div class=""info"">
                                <p>+84 988 889 898</p>
                                <p>fgoodsexchangeservices@gmail.com</p>
                                <p>Lô E2a-7, Đường D1, Đ. D1, Long Thạnh Mỹ, Thành Phố Thủ Đức, Thành phố Hồ Chí Minh 700000</p>
                            </div>
                        </div>
                    </div>
                </body>
                </html>
                ";
                return body;
            }

        }
    }
}

