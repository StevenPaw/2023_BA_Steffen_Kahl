<?php

namespace {

    use App\CharacterDatabase\CharacterPart;
    use Firebase\JWT\JWT;
    use Firebase\JWT\Key;
    use App\ExperienceDatabase\Experience;
    use App\ExperienceDatabase\ExperienceData;
    use App\ExperienceDatabase\ExperienceLocation;
    use App\ExperienceDatabase\LogEntry;
    use SilverStripe\Security\Member;
    use SilverStripe\Control\HTTPRequest;
    use SilverStripe\Core\Injector\Injector;
    use SilverStripe\Security\IdentityStore;
    use Level51\JWTUtils\JWTUtils;
    use Level51\JWTUtils\JWTUtilsException;
    use SilverStripe\Security\Security;
    use SilverStripe\CMS\Controllers\ContentController;

    /**
 * Class \PageController
 *
 * @property \ApiPage dataRecord
 * @method \ApiPage data()
 * @mixin \ApiPage
 */
    class ApiPageController extends ContentController
    {
        private static $allowed_actions = [
            "token",
            "account",
            "characterparts"
        ];

        public function index(HTTPRequest $request)
        {
            $data['API_Title'] = "Nordland-Games API";
            $data['API_Description'] = "This API is for communication between Nordland-Park games and the database";
            $data['API_Version'] = "1.0.0";

            $data['Copyright'] = "This API is developed and maintained by Steffen Kahl. All rights reserved.";

            $this->response->addHeader('Content-Type', 'application/json');
            $this->response->addHeader('Access-Control-Allow-Origin', '*');
            return json_encode($data);
        }

        public function characterparts(HTTPRequest $request)
        {
            $characterParts = CharacterPart::get();
            $data['Count'] = $characterParts->Count();
            $data['CharacterParts'] = $characterParts->toNestedArray();

            $this->response->addHeader('Content-Type', 'application/json');
            $this->response->addHeader('Access-Control-Allow-Origin', '*');

            return json_encode($data);
        }

        protected function init()
        {
            parent::init();
        }
    }
}
