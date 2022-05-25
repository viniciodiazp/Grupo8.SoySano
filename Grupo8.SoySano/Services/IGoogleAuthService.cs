using System;
using System.Collections.Generic;
using System.Text;

namespace Grupo8.SoySano.Services
{
    public interface IGoogleAuthService
    {
        void Autheticate(IGoogleAuthenticationDelegate googleAuthenticationDelegate);
    }
}
