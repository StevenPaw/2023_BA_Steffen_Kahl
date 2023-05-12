<?php

namespace {

    use App\Games\Game;
    use App\Games\HighScore;
    use App\CharacterDatabase\CharacterPart;
    use Firebase\JWT\JWT;
    use Firebase\JWT\Key;
    use App\ExperienceDatabase\Experience;
    use App\ExperienceDatabase\ExperienceData;
    use App\ExperienceDatabase\ExperienceLocation;
    use App\ExperienceDatabase\LogEntry;
    use App\Users\UserData;
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
            "characterparts",
            "changecharacter",
            "addhighscore",
            "addxp",
        ];

        public function index(HTTPRequest $request)
        {
            $data['API_Title'] = "Nordland-Games API";
            $data['API_Description'] = "This API is for communication between Nordland-Park games and the database";
            $data['API_Version'] = "1.0.0";

            $data['Copyright'] = "This API is developed and maintained by Steffen Kahl. All rights reserved.";

            $this->response->addHeader('Content-Type', 'application/json');
            //$this->response->addHeader('Access-Control-Allow-Origin', '*');
            return json_encode($data);
        }

        public function account(HTTPRequest $request)
        {
            $randomkey = "jsbcjdebfjnc38cnr89c";
            $userkey = $request->getVar("UserKey");
            $nickname = $request->getVar("Nickname");

            if ($userkey != null) {
                $user = UserData::get()->filter("UserKey", $userkey)->first();
                if ($user) {
                    $data['Status'] = "OK";
                    $data['User'] = $user->toMap();
                    $data['User']["AquiredCharacterParts"]['Count'] = $user->AquiredCharacterParts()->count();
                    unset($data['User']['ClassName']);
                    unset($data['User']['RecordClassName']);
                    unset($data['User']['ID']);
                    foreach ($user->AquiredCharacterParts() as $part) {
                        $data['User']['AquiredCharacterParts'][$part->ID] = $part->toMap();
                        $data['User']['AquiredCharacterParts'][$part->ID]['Image'] = $part->Image()->AbsoluteLink();
                        unset($data['User']['AquiredCharacterParts'][$part->ID]['ClassName']);
                        unset($data['User']['AquiredCharacterParts'][$part->ID]['ImageID']);
                        unset($data['User']['AquiredCharacterParts'][$part->ID]['RecordClassName']);
                        unset($data['User']['AquiredCharacterParts'][$part->ID]['Created']);
                    }
                } else {
                    $data['Status'] = "ERROR";
                    $data['Error'] = "User not found";
                }
            } else {
                if ($nickname != null) {
                    if (UserData::get()->filter("Nickname", $nickname)->first()) {
                        $data['Status'] = "ERROR";
                        $data['Error'] = "Nickname already in use";
                    } else {
                        $user = UserData::create();
                        $user->Nickname = $nickname;
                        $newuserkey = md5($nickname . time() . $randomkey);
                        $user->UserKey = $newuserkey;
                        $user->write();

                        $data['Status'] = "OK";
                        $data['User'] = $user->toMap();
                        $data['User']["AquiredCharacterParts"]['Count'] = $user->AquiredCharacterParts()->count();
                        unset($data['User']['ClassName']);
                        unset($data['User']['RecordClassName']);
                        unset($data['User']['ID']);
                        foreach ($user->AquiredCharacterParts() as $part) {
                            $data['User']['AquiredCharacterParts'][$part->ID] = $part->toMap();
                            $data['User']['AquiredCharacterParts'][$part->ID]['Image'] = $part->Image()->AbsoluteLink();
                            unset($data['User']['AquiredCharacterParts'][$part->ID]['ClassName']);
                            unset($data['User']['AquiredCharacterParts'][$part->ID]['ImageID']);
                            unset($data['User']['AquiredCharacterParts'][$part->ID]['RecordClassName']);
                            unset($data['User']['AquiredCharacterParts'][$part->ID]['Created']);
                        }
                    }
                } else {
                    $data['Status'] = "ERROR";
                    $data['Error'] = "No nickname or userkey provided";
                }
            }

            $this->response->addHeader('Content-Type', 'application/json');
            //$this->response->addHeader('Access-Control-Allow-Origin', '*');
            return json_encode($data);
        }

        public function characterparts(HTTPRequest $request)
        {
            $searchedid = $request->getVar("ID");
            if ($searchedid) {
                $characterPart = CharacterPart::get()->byID($searchedid);
                if ($characterPart) {
                    $data['Status'] = "OK";
                    $data['CharacterPart'] = $characterPart->toMap();
                    $data['CharacterPart']['Image'] = $characterPart->Image()->AbsoluteLink();
                    unset($data['CharacterPart']['ClassName']);
                    unset($data['CharacterPart']['ImageID']);
                    unset($data['CharacterPart']['RecordClassName']);
                    unset($data['CharacterPart']['Created']);
                } else {
                    $data['Status'] = "ERROR";
                    $data['Error'] = "No CharacterPart With This ID Found";
                }
            } else {
                $data['Status'] = "OK";
                $characterParts = CharacterPart::get();
                $data['Count'] = $characterParts->Count();
                //$data['CharacterParts'] = $characterParts->toNestedArray();

                foreach ($characterParts as $part) {
                    $data['CharacterParts'][$part->ID] = $part->toMap();
                    $data['CharacterParts'][$part->ID]['Image'] = $part->Image()->AbsoluteLink();
                    unset($data['CharacterParts'][$part->ID]['ClassName']);
                    unset($data['CharacterParts'][$part->ID]['ImageID']);
                    unset($data['CharacterParts'][$part->ID]['RecordClassName']);
                    unset($data['CharacterParts'][$part->ID]['Created']);
                }
            }

            $this->response->addHeader('Content-Type', 'application/json');
            //$this->response->addHeader('Access-Control-Allow-Origin', '*');
            return json_encode($data);
        }

        public function changecharacter(HTTPRequest $request)
        {
            $userkey = $request->getVar("UserKey");
            $characterpartid = $request->getVar("CharacterPartID");
            $characterparttype = $request->getVar("CharacterPartType");

            if ($userkey && $characterpartid && $characterparttype) {
                $user = UserData::get()->filter("UserKey", $userkey)->first();
                $characterpart = CharacterPart::get()->byID($characterpartid);

                if ($characterpart->Type === $characterparttype) {
                    $data['Status'] = "OK";
                    if ($characterparttype == "SkinColor") {
                        $user->SelectedSkinColorID = $characterpartid;
                    } else if ($characterparttype == "Eyes") {
                        $user->SelectedEyesID = $characterpartid;
                    } else if ($characterparttype == "Mouth") {
                        $user->SelectedMouthID = $characterpartid;
                    } else if ($characterparttype == "Hair") {
                        $user->SelectedHairID = $characterpartid;
                    } else if ($characterparttype == "Bottom") {
                        $user->SelectedBottomID = $characterpartid;
                    } else if ($characterparttype == "Top") {
                        $user->SelectedTopID = $characterpartid;
                    } else if ($characterparttype == "Hat") {
                        $user->SelectedHatID = $characterpartid;
                    } else if ($characterparttype == "BackDeco") {
                        $user->SelectedBackDecoID = $characterpartid;
                    }
                    $user->write();
                } else {
                    $data['Status'] = "ERROR";
                    $data['Error'] = "CharacterPart Type does not match";
                }
            } else {
                $data['Status'] = "ERROR";
                $data['Error'] = "No UserKey, CharacterPartID or CharacterPartType provided";
            }

            $this->response->addHeader('Content-Type', 'application/json');
            //$this->response->addHeader('Access-Control-Allow-Origin', '*');
            return json_encode($data);
        }

        public function addhighscore(HTTPRequest $request)
        {
            $userkey = $request->getVar("UserKey");
            $gameid = $request->getVar("GameID");
            $points = $request->getVar("Points");

            if ($userkey && $gameid && $points) {
                $user = UserData::get()->filter("UserKey", $userkey)->first();

                if ($user) {
                    $game = Game::get()->byID($gameid);

                    HighScore::create([
                        "Points" => $points,
                        "ParentID" => $gameid,
                        "UserID" => $user->ID
                    ])->write();
                    $data['Status'] = "OK";
                } else {
                    $data['Status'] = "ERROR";
                    $data['Error'] = "No User With This UserKey Found";
                }
            } else {
                $data['Status'] = "ERROR";
                $data['Error'] = "No UserKey, GameID or Points provided";
            }

            $this->response->addHeader('Content-Type', 'application/json');
            //$this->response->addHeader('Access-Control-Allow-Origin', '*');
            return json_encode($data);
        }

        public function addxp(HTTPRequest $request)
        {
            $userkey = $request->getVar("UserKey");
            $xp = $request->getVar("XP");

            if ($userkey && $xp) {
                $user = UserData::get()->filter("UserKey", $userkey)->first();

                if ($user) {
                    $currentXP = $user->XP;

                    $user->XP = $currentXP + $xp;
                    $user->write();

                    $data['Status'] = "OK";
                } else {
                    $data['Status'] = "ERROR";
                    $data['Error'] = "No User With This UserKey Found";
                }
            } else {
                $data['Status'] = "ERROR";
                $data['Error'] = "No UserKey or XP provided";
            }

            $this->response->addHeader('Content-Type', 'application/json');
            //$this->response->addHeader('Access-Control-Allow-Origin', '*');
            return json_encode($data);
        }

        protected function init()
        {
            parent::init();
        }
    }
}
